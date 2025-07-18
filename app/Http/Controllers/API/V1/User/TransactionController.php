<?php

namespace App\Http\Controllers\API\V1\User;

use App\Http\Controllers\Controller;
use App\Http\Requests\API\V1\User\StoreTransactionRequest;
use App\Http\Requests\API\V1\User\UpdateTransactionRequest;
use App\Services\CategoryService;
use App\Services\TransactionService;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Response;

class TransactionController extends Controller
{
    public function __construct(
        private TransactionService $transactionService,
        private CategoryService $categoryService
    ) {}

    public function index(Request $request)
    {
        $userId = $request->user()->id;
        $transactions = $this->transactionService->getAllByUserId($userId);

        return Response::json($transactions, 200);
    }

    public function store(StoreTransactionRequest $request)
    {
        $data = $request->validated();
        $category = $this->categoryService->findByCategoryIdAndUserId($data['category_id'], $request->user()->id);

        if (!$category) {
            return Response::json(['message' => 'Category not found'], 404);
        }

        $transaction = $this->transactionService->create($data, $request->user(), $category);

        return Response::json($transaction, 201);
    }

    public function show(Request $request, int $id)
    {
        $userId = $request->user()->id;
        $transaction = $this->transactionService->findByTransactionIdAndUserId($id, $userId);

        if (!$transaction) {
            return Response::json(['message' => 'Transaction not found'], 404);
        }

        return Response::json($transaction, 200);
    }

    public function update(UpdateTransactionRequest $request, int $id)
    {
        $userId = $request->user()->id;
        $transaction = $this->transactionService->findByTransactionIdAndUserId($id, $userId);

        if (!$transaction) {
            return Response::json(['message' => 'Transaction not found'], 404);
        }

        $data = $request->validated();
        $category = $this->categoryService->findByCategoryIdAndUserId($data['category_id'], $userId);

        if (!$category) {
            return Response::json(['message' => 'Category not found'], 404);
        }

        $this->transactionService->update($transaction, $data, $category);

        return Response::json($transaction, 200);
    }

    public function destroy(Request $request, int $id)
    {
        $userId = $request->user()->id;
        $transaction = $this->transactionService->findByTransactionIdAndUserId($id, $userId);

        if (!$transaction) {
            return Response::json(['message' => 'Transaction not found'], 404);
        }

        $this->transactionService->delete($transaction);

        return Response::json(null, 204);
    }
}
