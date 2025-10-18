<?php

namespace App\Http\Controllers\Auth;

use Exception;
use App\Models\User;
use Illuminate\Support\Str;
use Illuminate\Http\Request;
use App\Mail\PasswordResetMail;
use Illuminate\Support\Facades\Log;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Mail;
use Illuminate\Support\Facades\Storage;
use App\Http\Resources\AuthUserResource;
use Illuminate\Validation\ValidationException;

class AuthController extends Controller
{
    /**
     * Login a user
     */
    public function login(Request $request)
    {
        $request->validate([
            'email' => 'required|string|email',
            'password' => 'required|string',
        ]);

        if (Auth::attempt($request->only('email', 'password'))) {
            $user = Auth::user();
            return new AuthUserResource($user);
        }

        throw ValidationException::withMessages([
            'email' => ['The provided credentials are incorrect.'],
        ]);
    }

    /**
     * Logout the current user
     */
    public function logout(Request $request)
    {
        Auth::logout();

        $request->session()->invalidate();
        $request->session()->regenerateToken();

        return response()->json([], 200);
    }

    /**
     * Update the specified resource in storage.
     *
     * @return \Illuminate\Http\Response
     */
    public function updatePassword(Request $request)
    {
        $data = $request->validate([
            'password' => 'required|string|min:8|current_password:sanctum',
            'new_password' => 'required|string|min:8',
        ]);

        $data = ['password' => Hash::make($request->new_password)];

        try {
            auth()->user()->update($data);

            return response()->json(['message' => 'Password updated successfully'], 200);
        } catch (Exception $e) {
            Log::error('Error updating password');
            Log::debug($e->getMessage());
            Log::debug($e->getTraceAsString());

            return response()->json([
                'message' => 'Error updating password',
            ], 400);
        }
    }

    public function forgotPassword(Request $request)
    {
        $request->validate([
            'email' => 'required|email'
        ]);

        try {
            $user = User::where('email', $request->email)->firstOrFail();

            // Generate a temporary password
            $tempPassword = Str::random(10);

            $user->update([
                'password' => Hash::make($tempPassword),
                // 'password_reset_required' => true TOOD
            ]);

            Mail::to($user)->send(new PasswordResetMail([
                'user' => $user->first_name,
                'tempPassword' => $tempPassword
            ]));

            return response()->json([
                'message' => 'Password reset successfully. Check your email for the temporary password.',
            ]);
        } catch (\Exception $e) {
            Log::error('Failed to reset password for email: ' . $request->email);
            Log::debug($e->getMessage());
            Log::debug($e->getTraceAsString());

            return response()->json([
                'message' => 'Failed to reset password. Please try again later.'
            ], 400);
        }
    }

    public function getSignature()
    {
        $id = auth()->id();
        $path = "signatures/signature_{$id}.png";

        if (!Storage::exists($path)) {
            return response()->json(['message' => 'Signature not found'], 404);
        }

        $file = Storage::disk('local')->get($path);

        return response($file, 200)->header('Content-Type', 'image/png');
    }

    public function updateSignature(Request $request)
    {
        $data = $request->validate([
            'signature_credentials' => 'nullable|string|max:50',
            'signature' => 'nullable|regex:/^data:image\/(png);base64,([A-Za-z0-9+\/=]+)$/',
        ]);

        try {
            auth()->user()->update(['signature_credentials' => $data['signature_credentials'] ?? null]);

            if ($request->signature) {
                $imageData = substr($request->signature, strpos($request->signature, ',') + 1);
                $decodedData = base64_decode($imageData, true);

                // Generate unique filename
                $fileName = 'signature_' . auth()->id() . '.png';

                // Save to public disk
                Storage::put("signatures/{$fileName}", $decodedData);
            }

            return response()->json([
                'message' => 'Signature updated successfully',
                'signature_url' => auth()->user()->signature_url
            ], 200);
        } catch (Exception $e) {
            Log::error('Error updating signature');
            Log::debug($e->getMessage());
            Log::debug($e->getTraceAsString());

            return response()->json([
                'message' => 'Error updating signature',
            ], 400);
        }
    }
}
