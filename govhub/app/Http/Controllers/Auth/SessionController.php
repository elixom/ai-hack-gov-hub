<?php

namespace App\Http\Controllers\Auth;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use App\Http\Controllers\Controller;
use App\Http\Resources\UserResource;
use App\Http\Resources\SessionCollection;

class SessionController extends Controller
{
    public function __invoke()
    {
        return new UserResource(auth()->user());
    }

    /**
     * Get the current sessions.
     */
    public function sessions(Request $request)
    {
        if (config('session.driver') !== 'database') {
            return collect();
        }

        $sessions = DB::connection(config('session.connection'))->table(config('session.table', 'sessions'))
            ->where('user_id', auth()->id())
            ->orderBy('last_activity', 'desc')
            ->get();

        return new SessionCollection($sessions);
    }

    /**
     * Logout other sessions.
     *
     * @return \Illuminate\Http\Response
     */
    public function logoutOtherSessions(Request $request)
    {
        if (config('session.driver') !== 'database') {
            return response()->json(['message' => 'This feature is only supported with the database session driver.'], 400);
        }

        DB::connection(config('session.connection'))->table(config('session.table', 'sessions'))
            ->where('user_id', auth()->id())
            ->where('id', '!=', $request->session()->getId())
            ->delete();


        return response()->json(['message' => 'Logged out other browser sessions.']);
    }
}
