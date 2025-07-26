<?php

namespace App\Http\Controllers\API\Auth;

use App\Http\Controllers\Controller;
use App\Http\Resources\User\UserResource;
use App\Traits\ApiResponse;
use Carbon\Carbon;
use Illuminate\Http\Request;

class RefreshTokenController extends Controller
{
    use ApiResponse;

    public function __invoke(Request $request)
    {
        $user = $request->user();

        $personalAccessToken = $user->createToken(
            'personal_access_token',
            ['api'],
            Carbon::now()->addMinutes(60)
        )->plainTextToken;
        $refreshToken = $request->bearerToken();

        return $this->response(200, [
            'user' => new UserResource($user),
            'personal_access_token' => $personalAccessToken,
            'refresh_token' => $refreshToken
        ]);
    }
}
