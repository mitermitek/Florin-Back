<?php

namespace App\Repositories;

use App\Models\Category;
use App\Models\Transaction;
use App\Models\User;
use Illuminate\Database\Eloquent\Collection;

class TransactionRepository
{
    public function __construct(private Transaction $transaction) {}

    public function getAllByUserId(int $userId): Collection
    {
        return $this->transaction->with(['category'])->where('user_id', $userId)->get();
    }

    public function create(array $data, User $user, Category $category): Transaction
    {
        $transaction = $this->transaction->newInstance($data);
        $transaction->user()->associate($user);
        $transaction->category()->associate($category);
        $transaction->save();

        return $transaction;
    }

    public function findByTransactionIdAndUserId(int $transactionId, int $userId): ?Transaction
    {
        return $this->transaction->with(['category'])->where('id', $transactionId)->where('user_id', $userId)->first();
    }

    public function update(Transaction $transaction, array $data, Category $category): Transaction
    {
        $transaction->update($data);
        $transaction->category()->associate($category);
        $transaction->save();

        return $transaction;
    }

    public function delete(Transaction $transaction): bool
    {
        return $transaction->delete();
    }
}
