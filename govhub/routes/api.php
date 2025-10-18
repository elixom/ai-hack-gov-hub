<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\Auth\AuthController;
use App\Http\Controllers\Auth\SessionController;


// ============================================================================
// AUTHENTICATION ROUTES
// ============================================================================
Route::prefix('auth')->group(function () {
    // Public authentication routes
    Route::post('/login', [AuthController::class, 'login']);
    Route::post('/logout', [AuthController::class, 'logout']);
    Route::post('/forgot-password', [AuthController::class, 'forgotPassword']);

    // Protected authentication routes
    Route::middleware(['auth', 'throttle:6,2'])->group(function () {
        // user
        Route::put('/user/password', [AuthController::class, 'updatePassword'])->middleware('throttle:4,120');

        // active session
        Route::get('/sessions', [SessionController::class, 'sessions']);
        Route::get('/sessions/current', SessionController::class);
        Route::post('/sessions', [SessionController::class, 'logoutOtherSessions']);
    });
});
