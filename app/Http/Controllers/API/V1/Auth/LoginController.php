<?php

namespace App\Http\Controllers\API\V1\Auth;

use App\Http\Controllers\Controller;
use App\Http\Requests\API\V1\Auth\LoginRequest;
use App\Services\UserService;
use Illuminate\Support\Facades\Response;

class LoginController extends Controller
{
    public function __construct(private UserService $userService) {}

    public function __invoke(LoginRequest $request)
    {
        $credentials = $request->validated();
        $token = $this->userService->authenticate($credentials);

        if (!$token) {
            return Response::json(['message' => 'Unauthorized'], 401);
        }

        return Response::json(['token' => $token], 200);
    }
}
