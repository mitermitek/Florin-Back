<?php

namespace App\Http\Controllers\API\V1\User;

use App\Http\Controllers\Controller;
use App\Http\Requests\API\V1\User\StoreTransactionRequest;
use App\Http\Requests\API\V1\User\UpdateTransactionRequest;
use App\Traits\ApiResponse;
use Illuminate\Http\Request;
use Illuminate\Support\Arr;

class TransactionController extends Controller
{
    use ApiResponse;

    public function index(Request $request)
    {
        return $this->response(200, 'Transactions retrieved successfully', [
            'transactions' => $request->user()->transactions,
        ]);
    }

    public function store(StoreTransactionRequest $request)
    {
        $data = $request->validated();

        $transaction = $request->user()->transactions()->make(
            Arr::except($data, 'category_id')
        );
        $transaction->category()->associate($data['category_id']);
        $transaction->save();

        return $this->response(201, 'Transaction created successfully', [
            'transaction' => $transaction,
        ]);
    }

    public function show(Request $request, int $id)
    {
        $transaction = $request->user()->transactions()->find($id);

        if (!$transaction) {
            return $this->response(404, 'Transaction not found');
        }

        return $this->response(200, 'Transaction retrieved successfully', [
            'transaction' => $transaction,
        ]);
    }

    public function update(UpdateTransactionRequest $request, int $id)
    {
        $transaction = $request->user()->transactions()->find($id);

        if (!$transaction) {
            return $this->response(404, 'Transaction not found');
        }

        $data = $request->validated();

        $transaction->fill(Arr::except($data, 'category_id'));
        $transaction->category()->associate($data['category_id']);
        $transaction->save();

        return $this->response(200, 'Transaction updated successfully', [
            'transaction' => $transaction,
        ]);
    }

    public function destroy(Request $request, int $id)
    {
        $transaction = $request->user()->transactions()->find($id);

        if (!$transaction) {
            return $this->response(404, 'Transaction not found');
        }

        $transaction->delete();

        return $this->response(200, 'Transaction deleted successfully');
    }
}
