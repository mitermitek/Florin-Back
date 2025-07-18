<?php

namespace App\Http\Controllers\API\V1\Auth;

use App\Http\Controllers\Controller;
use App\Http\Requests\API\V1\Auth\RegisterRequest;
use App\Models\User;
use App\Traits\ApiResponse;
use Illuminate\Support\Facades\Hash;

class RegisterController extends Controller
{
    use ApiResponse;

    public function __invoke(RegisterRequest $request)
    {
        $data = $request->validated();
        $data['first_name'] = ucfirst($data['first_name']);
        $data['last_name'] = strtoupper($data['last_name']);
        $data['email'] = strtolower($data['email']);
        $data['password'] = Hash::make($data['password']);

        $user = User::create($data);

        return $this->response(201, 'User registered successfully', [
            'user' => $user
        ]);
    }
}
