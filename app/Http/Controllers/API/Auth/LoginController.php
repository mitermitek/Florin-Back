<?php

namespace App\Http\Controllers\API\Auth;

use App\Http\Controllers\Controller;
use App\Http\Requests\API\Auth\LoginRequest;
use App\Http\Resources\User\UserResource;
use App\Models\User;
use App\Traits\ApiResponse;
use Carbon\Carbon;
use Illuminate\Support\Facades\Hash;

class LoginController extends Controller
{
    use ApiResponse;

    public function __invoke(LoginRequest $request)
    {
        $credentials = $request->validated();
        $user = User::where('email', $credentials['email'])->first();

        if (!$user || !Hash::check($credentials['password'], $user->password)) {
            return $this->response(401);
        }

        $personalAccessToken = $user->createToken(
            'personal_access_token',
            ['api'],
            Carbon::now()->addMinutes(60)
        )->plainTextToken;
        $refreshToken = $user->createToken(
            'refresh_token',
            ['refresh'],
            Carbon::now()->addDays(7)
        )->plainTextToken;

        return $this->response(200, [
            'user' => new UserResource($user),
            'personal_access_token' => $personalAccessToken,
            'refresh_token' => $refreshToken
        ]);
    }
}
