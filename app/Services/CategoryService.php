<?php

namespace App\Services;

use App\Models\Category;
use App\Models\User;
use App\Repositories\CategoryRepository;
use Illuminate\Database\Eloquent\Collection;

class CategoryService
{
    public function __construct(private CategoryRepository $categoryRepository) {}

    public function getAllByUserId(int $userId): Collection
    {
        return $this->categoryRepository->getAllByUserId($userId);
    }

    public function create(array $data, User $user): Category
    {
        return $this->categoryRepository->create($data, $user);
    }

    public function findByCategoryIdAndUserId(int $categoryId, int $userId): ?Category
    {
        return $this->categoryRepository->findByCategoryIdAndUserId($categoryId, $userId);
    }

    public function update(Category $category, array $data): Category
    {
        return $this->categoryRepository->update($category, $data);
    }

    public function delete(Category $category): bool
    {
        return $this->categoryRepository->delete($category);
    }
}
