<?php

namespace App\Http\Controllers\API\V1\User;

use App\Http\Controllers\Controller;
use App\Http\Requests\API\V1\User\StoreCategoryRequest;
use App\Http\Requests\API\V1\User\UpdateCategoryRequest;
use App\Http\Resources\Category\CategoryResource;
use App\Traits\ApiResponse;
use Illuminate\Http\Request;

class CategoryController extends Controller
{
    use ApiResponse;

    public function index(Request $request)
    {
        $query = $request->user()->categories();

        // Filter by name (partial search)
        if ($request->filled('name')) {
            $query->where('name', 'like', '%' . $request->name . '%');
        }

        $categories = $query
            ->orderBy('name', 'asc')
            ->paginate(10);

        return $this->response(200, CategoryResource::collection($categories));
    }

    public function store(StoreCategoryRequest $request)
    {
        return $this->response(201, new CategoryResource($request->user()->categories()->create($request->validated())));
    }

    public function show(Request $request, int $id)
    {
        $category = $request->user()->categories()->find($id);

        if (!$category) {
            return $this->response(404);
        }

        return $this->response(200, new CategoryResource($category));
    }

    public function update(UpdateCategoryRequest $request, int $id)
    {
        $category = $request->user()->categories()->find($id);

        if (!$category) {
            return $this->response(404);
        }

        $category->update($request->validated());

        return $this->response(200, new CategoryResource($category));
    }

    public function destroy(Request $request, int $id)
    {
        $category = $request->user()->categories()->find($id);

        if (!$category) {
            return $this->response(404);
        }

        $category->delete();

        return $this->response(204);
    }
}
