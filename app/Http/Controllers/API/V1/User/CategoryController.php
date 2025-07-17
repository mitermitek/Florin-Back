<?php

namespace App\Http\Controllers\API\V1\User;

use App\Http\Controllers\Controller;
use App\Http\Requests\API\V1\User\StoreCategoryRequest;
use App\Http\Requests\API\V1\User\UpdateCategoryRequest;
use App\Services\CategoryService;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Response;

class CategoryController extends Controller
{
    public function __construct(private CategoryService $categoryService) {}

    public function index(Request $request)
    {
        $userId = $request->user()->id;
        $categories = $this->categoryService->getAllByUserId($userId);

        return Response::json($categories, 200);
    }

    public function store(StoreCategoryRequest $request)
    {
        $data = $request->validated();
        $category = $this->categoryService->create($data, $request->user());

        return Response::json($category, 201);
    }

    public function show(Request $request, int $id)
    {
        $userId = $request->user()->id;
        $category = $this->categoryService->findByCategoryIdAndUserId($id, $userId);

        if (!$category) {
            return Response::json(['message' => 'Category not found'], 404);
        }

        return Response::json($category, 200);
    }

    public function update(UpdateCategoryRequest $request, int $id)
    {
        $userId = $request->user()->id;
        $category = $this->categoryService->findByCategoryIdAndUserId($id, $userId);

        if (!$category) {
            return Response::json(['message' => 'Category not found'], 404);
        }

        $data = $request->validated();
        $this->categoryService->update($category, $data);

        return Response::json($category, 200);
    }

    public function destroy(Request $request, int $id)
    {
        $userId = $request->user()->id;
        $category = $this->categoryService->findByCategoryIdAndUserId($id, $userId);

        if (!$category) {
            return Response::json(['message' => 'Category not found'], 404);
        }

        $this->categoryService->delete($category);

        return Response::json(null, 204);
    }
}
