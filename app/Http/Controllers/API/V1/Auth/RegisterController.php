<?php

namespace App\Http\Controllers\API\V1\Auth;

use App\Http\Controllers\Controller;
use App\Http\Requests\API\V1\Auth\RegisterRequest;
use App\Models\User;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Response;

class RegisterController extends Controller
{
    public function __invoke(RegisterRequest $request)
    {
        $data = $request->validated();
        $data['first_name'] = ucfirst($data['first_name']);
        $data['last_name'] = strtoupper($data['last_name']);
        $data['email'] = strtolower($data['email']);
        $data['password'] = Hash::make($data['password']);

        $user = User::create($data);

        return Response::json($user, 201);
    }
}
