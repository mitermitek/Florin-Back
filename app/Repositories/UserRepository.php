<?php

namespace App\Repositories;

use App\Models\User;

class UserRepository
{
    public function __construct(private User $user) {}

    public function create(array $data): User
    {
        return $this->user->create($data);
    }

    public function findByEmail(string $email): ?User
    {
        return $this->user->where('email', $email)->first();
    }
}
