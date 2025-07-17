<?php

use App\Http\Controllers\API\V1\Auth\LoginController;
use App\Http\Controllers\API\V1\Auth\RegisterController;
use App\Http\Controllers\API\V1\User\CategoryController as UserCategoryController;
use App\Models\Category;
use Illuminate\Support\Facades\Route;

Route::prefix('v1')->group(function () {
    Route::prefix('auth')->group(function () {
        Route::post('register', RegisterController::class);
        Route::post('login', LoginController::class);
    });

    Route::middleware('auth:sanctum')->group(function () {
        Route::prefix('user')->group(function () {
            Route::prefix('categories')->group(function () {
                Route::get('/', [UserCategoryController::class, 'index']);
                Route::post('/', [UserCategoryController::class, 'store']);
                Route::get('/{id}', [UserCategoryController::class, 'show']);
                Route::put('/{id}', [UserCategoryController::class, 'update']);
                Route::delete('/{id}', [UserCategoryController::class, 'destroy']);
            });
        });
    });
});
