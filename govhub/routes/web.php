<?php

use Illuminate\Support\Facades\Route;

// All routes will be handled by Vue.js router
Route::get('/{any}', function () {
    return view('app');
})->where('any', '.*');
