<?php

namespace App\Http\Controllers\API\V1\Auth;

use App\Http\Controllers\Controller;
use App\Http\Requests\API\V1\Auth\RegisterRequest;
use App\Services\UserService;
use Illuminate\Support\Facades\Response;

class RegisterController extends Controller
{
    public function __construct(private UserService $userService) {}

    public function __invoke(RegisterRequest $request)
    {
        $data = $request->validated();
        $user = $this->userService->create($data);

        return Response::json($user, 201);
    }
}
