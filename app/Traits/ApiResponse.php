<?php

namespace App\Traits;

use Illuminate\Http\JsonResponse;
use Illuminate\Support\Facades\Response;

trait ApiResponse
{
    protected function response(int $status, string $message = '', array $data = []): JsonResponse
    {
        return Response::json([
            'message' => $message,
            'data' => $data,
        ], $status);
    }
}
