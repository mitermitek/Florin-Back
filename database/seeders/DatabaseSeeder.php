<?php

namespace Database\Seeders;

use App\Models\Category;
use App\Models\Transaction;
use App\Models\User;
// use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     */
    public function run(): void
    {
        User::factory(10)->create();

        User::factory()->create([
            'first_name' => 'Test',
            'last_name' => 'User',
            'email' => 'test@example.com',
        ]);

        Category::factory(100)->make()->each(function ($category) {
            $category->user()->associate(User::inRandomOrder()->first());
            $category->save();
        });

        Transaction::factory(500)->make()->each(function ($transaction) {
            $user = User::inRandomOrder()->with(['categories'])->first();
            $category = $user->categories->random();

            $transaction->user()->associate($user);
            $transaction->category()->associate($category);
            $transaction->save();
        });
    }
}
