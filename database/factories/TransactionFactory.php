<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;

/**
 * @extends \Illuminate\Database\Eloquent\Factories\Factory<\App\Models\Transaction>
 */
class TransactionFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition(): array
    {
        return [
            'type' => $this->faker->randomElement(['income', 'expense']),
            'date' => $this->faker->dateTimeBetween('-3 months', 'now')->format('Y-m-d'),
            'amount' => $this->faker->randomFloat(2, 0, 1000),
            'description' => $this->faker->words(3, true)
        ];
    }
}
