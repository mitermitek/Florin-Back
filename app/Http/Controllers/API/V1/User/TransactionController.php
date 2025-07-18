<?php

namespace App\Http\Controllers\API\V1\User;

use App\Http\Controllers\Controller;
use App\Http\Requests\API\V1\User\StoreTransactionRequest;
use App\Http\Requests\API\V1\User\UpdateTransactionRequest;
use App\Models\Transaction;
use Illuminate\Http\Request;
use Illuminate\Support\Arr;
use Illuminate\Support\Facades\Response;

class TransactionController extends Controller
{
    public function index(Request $request)
    {
        return Response::json($request->user()->transactions, 200);
    }

    public function store(StoreTransactionRequest $request)
    {
        $data = $request->validated();

        $transaction = $request->user()->transactions()->make(
            Arr::except($data, 'category_id')
        );
        $transaction->category()->associate($data['category_id']);
        $transaction->save();

        return Response::json($transaction, 201);
    }

    public function show(Request $request, int $id)
    {
        $transaction = $request->user()->transactions()->find($id);

        if (!$transaction) {
            return Response::json(['message' => 'Transaction not found'], 404);
        }

        return Response::json($transaction, 200);
    }

    public function update(UpdateTransactionRequest $request, int $id)
    {
        $transaction = $request->user()->transactions()->find($id);

        if (!$transaction) {
            return Response::json(['message' => 'Transaction not found'], 404);
        }

        $data = $request->validated();

        $transaction->fill(Arr::except($data, 'category_id'));
        $transaction->category()->associate($data['category_id']);
        $transaction->save();

        return Response::json($transaction, 200);
    }

    public function destroy(Request $request, int $id)
    {
        $transaction = $request->user()->transactions()->find($id);

        if (!$transaction) {
            return Response::json(['message' => 'Transaction not found'], 404);
        }

        $transaction->delete();

        return Response::json(null, 204);
    }
}
