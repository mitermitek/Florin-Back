<?php

namespace App\Traits;

use Illuminate\Http\JsonResponse;
use Illuminate\Http\Resources\Json\JsonResource;
use Illuminate\Support\Facades\Response;

trait ApiResponse
{
    protected function response(int $status, JsonResource|array|null $data = null): JsonResponse
    {
        if ($data instanceof JsonResource) {
            return $data->response()->setStatusCode($status);
        }

        return Response::json($data, $status);
    }
}
