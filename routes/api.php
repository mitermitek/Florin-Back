<?php

use App\Http\Controllers\API\Auth\LoginController;
use App\Http\Controllers\API\Auth\LogoutController;
use App\Http\Controllers\API\Auth\RefreshTokenController;
use App\Http\Controllers\API\Auth\RegisterController;
use App\Http\Controllers\API\User\UserCategoryController;
use App\Http\Controllers\API\User\UserTransactionController;
use Illuminate\Support\Facades\Route;

Route::prefix('auth')->group(function () {
    Route::post('register', RegisterController::class);
    Route::post('login', LoginController::class);

    Route::middleware('auth:sanctum', 'ability:api')->group(function () {
        Route::delete('logout', LogoutController::class);
    });

    Route::get('refresh-token', RefreshTokenController::class)->middleware('auth:sanctum', 'ability:refresh');
});

Route::middleware('auth:sanctum', 'ability:api')->group(function () {
    Route::prefix('user')->group(function () {
        Route::prefix('categories')->group(function () {
            Route::get('/', [UserCategoryController::class, 'index']);
            Route::post('/', [UserCategoryController::class, 'store']);
            Route::get('/{id}', [UserCategoryController::class, 'show']);
            Route::put('/{id}', [UserCategoryController::class, 'update']);
            Route::delete('/{id}', [UserCategoryController::class, 'destroy']);
        });

        Route::prefix('transactions')->group(function () {
            Route::get('/', [UserTransactionController::class, 'index']);
            Route::post('/', [UserTransactionController::class, 'store']);
            Route::get('/{id}', [UserTransactionController::class, 'show']);
            Route::put('/{id}', [UserTransactionController::class, 'update']);
            Route::delete('/{id}', [UserTransactionController::class, 'destroy']);
        });
    });
});
