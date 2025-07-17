<?php

namespace App\Repositories;

use App\Models\Category;
use App\Models\User;
use Illuminate\Database\Eloquent\Collection;

class CategoryRepository
{
    public function __construct(private Category $category) {}

    public function getAllByUserId(int $userId): Collection
    {
        return $this->category->where('user_id', $userId)->get();
    }

    public function create(array $data, User $user): Category
    {
        $category = $this->category->newInstance($data);
        $category->user()->associate($user);
        $category->save();

        return $category;
    }

    public function findByCategoryIdAndUserId(int $categoryId, int $userId): ?Category
    {
        return $this->category->where('id', $categoryId)->where('user_id', $userId)->first();
    }

    public function update(Category $category, array $data): Category
    {
        $category->update($data);
        return $category;
    }

    public function delete(Category $category): bool
    {
        return $category->delete();
    }
}
