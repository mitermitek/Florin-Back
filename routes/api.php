<?php

use App\Http\Controllers\API\V1\Auth\LoginController;
use App\Http\Controllers\API\V1\Auth\RegisterController;
use App\Http\Controllers\API\V1\User\CategoryController as UserCategoryController;
use App\Http\Controllers\API\V1\User\TransactionController as UserTransactionController;
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

            Route::prefix('transactions')->group(function () {
                Route::get('/', [UserTransactionController::class, 'index']);
                Route::post('/', [UserTransactionController::class, 'store']);
                Route::get('/{id}', [UserTransactionController::class, 'show']);
                Route::put('/{id}', [UserTransactionController::class, 'update']);
                Route::delete('/{id}', [UserTransactionController::class, 'destroy']);
            });
        });
    });
});
