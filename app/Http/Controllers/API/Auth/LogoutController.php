<?php

namespace App\Http\Controllers\API\Auth;

use App\Http\Controllers\Controller;
use App\Traits\ApiResponse;
use Illuminate\Http\Request;

class LogoutController extends Controller
{
    use ApiResponse;

    public function __invoke(Request $request)
    {
        $user = $request->user();

        if ($user) {
            $user->currentAccessToken()->delete();
        }

        return $this->response(204);
    }
}
