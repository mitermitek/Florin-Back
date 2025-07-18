<?php

namespace App\Http\Controllers\API\V1\User;

use App\Http\Controllers\Controller;
use App\Http\Requests\API\V1\User\StoreCategoryRequest;
use App\Http\Requests\API\V1\User\UpdateCategoryRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Response;

class CategoryController extends Controller
{
    public function index(Request $request)
    {
        return Response::json($request->user()->categories, 200);
    }

    public function store(StoreCategoryRequest $request)
    {
        return Response::json($request->user()->categories()->create($request->validated()), 201);
    }

    public function show(Request $request, int $id)
    {
        $category = $request->user()->categories()->find($id);

        if (!$category) {
            return Response::json(['message' => 'Category not found'], 404);
        }

        return Response::json($category, 200);
    }

    public function update(UpdateCategoryRequest $request, int $id)
    {
        $category = $request->user()->categories()->find($id);

        if (!$category) {
            return Response::json(['message' => 'Category not found'], 404);
        }

        $category->update($request->validated());

        return Response::json($category, 200);
    }

    public function destroy(Request $request, int $id)
    {
        $category = $request->user()->categories()->find($id);

        if (!$category) {
            return Response::json(['message' => 'Category not found'], 404);
        }

        $category->delete();

        return Response::json(null, 204);
    }
}
