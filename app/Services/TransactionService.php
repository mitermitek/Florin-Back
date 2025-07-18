<?php

namespace App\Services;

use App\Models\Category;
use App\Models\Transaction;
use App\Models\User;
use App\Repositories\TransactionRepository;
use Illuminate\Database\Eloquent\Collection;

class TransactionService
{
    public function __construct(private TransactionRepository $transactionRepository) {}

    public function getAllByUserId(int $userId): Collection
    {
        return $this->transactionRepository->getAllByUserId($userId);
    }

    public function create(array $data, User $user, Category $category): Transaction
    {
        return $this->transactionRepository->create($data, $user, $category);
    }

    public function findByTransactionIdAndUserId(int $transactionId, int $userId): ?Transaction
    {
        return $this->transactionRepository->findByTransactionIdAndUserId($transactionId, $userId);
    }

    public function update(Transaction $transaction, array $data, Category $category): Transaction
    {
        return $this->transactionRepository->update($transaction, $data, $category);
    }

    public function delete(Transaction $transaction): bool
    {
        return $this->transactionRepository->delete($transaction);
    }
}
