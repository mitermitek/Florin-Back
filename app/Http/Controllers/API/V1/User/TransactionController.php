<?php

namespace App\Http\Controllers\API\V1\User;

use App\Http\Controllers\Controller;
use App\Http\Requests\API\V1\User\StoreTransactionRequest;
use App\Http\Requests\API\V1\User\UpdateTransactionRequest;
use App\Http\Resources\Transaction\TransactionResource;
use App\Traits\ApiResponse;
use Illuminate\Http\Request;
use Illuminate\Support\Arr;

class TransactionController extends Controller
{
    use ApiResponse;

    public function index(Request $request)
    {
        $query = $request->user()->transactions();

        // Filter by category
        if ($request->filled('category_id')) {
            $query->where('category_id', $request->category_id);
        }

        // Filter by description (partial search)
        if ($request->filled('description')) {
            $query->where('description', 'like', '%' . $request->description . '%');
        }

        // Filter by date (exact date)
        if ($request->filled('date')) {
            $query->whereDate('date', $request->date);
        }

        // Filter by period (date_from and date_to)
        if ($request->filled('date_from')) {
            $query->whereDate('date', '>=', $request->date_from);
        }
        if ($request->filled('date_to')) {
            $query->whereDate('date', '<=', $request->date_to);
        }

        // Filter by transaction type
        if ($request->filled('type')) {
            $query->where('type', $request->type);
        }

        $transactions = $query
            ->orderByDesc('date')
            ->orderByDesc('created_at')
            ->paginate(10);

        return $this->response(200, TransactionResource::collection($transactions));
    }

    public function store(StoreTransactionRequest $request)
    {
        $data = $request->validated();

        $transaction = $request->user()->transactions()->make(
            Arr::except($data, 'category_id')
        );
        $transaction->category()->associate($data['category_id']);
        $transaction->save();

        return $this->response(201, new TransactionResource($transaction));
    }

    public function show(Request $request, int $id)
    {
        $transaction = $request->user()->transactions()->find($id);

        if (!$transaction) {
            return $this->response(404);
        }

        return $this->response(200, new TransactionResource($transaction));
    }

    public function update(UpdateTransactionRequest $request, int $id)
    {
        $transaction = $request->user()->transactions()->find($id);

        if (!$transaction) {
            return $this->response(404);
        }

        $data = $request->validated();

        $transaction->fill(Arr::except($data, 'category_id'));
        $transaction->category()->associate($data['category_id']);
        $transaction->save();

        return $this->response(200, new TransactionResource($transaction));
    }

    public function destroy(Request $request, int $id)
    {
        $transaction = $request->user()->transactions()->find($id);

        if (!$transaction) {
            return $this->response(404);
        }

        $transaction->delete();

        return $this->response(204);
    }
}
