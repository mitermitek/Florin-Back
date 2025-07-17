<?php

namespace App\Services;

use App\Models\User;
use App\Repositories\UserRepository;
use Illuminate\Support\Facades\Hash;

class UserService
{
    public function __construct(private UserRepository $userRepository) {}

    public function authenticate(array $credentials): ?string
    {
        $user = $this->userRepository->findByEmail($credentials['email']);

        if (!$user || !Hash::check($credentials['password'], $user->password)) {
            return null;
        }

        return $user->createToken('FLR_PAT')->plainTextToken;
    }

    public function create(array $data): User
    {
        $data = $this->formatDataBeforeCreation($data);
        $data['password'] = Hash::make($data['password']);

        return $this->userRepository->create($data);
    }

    private function formatDataBeforeCreation(array $data): array
    {
        $data['first_name'] = ucfirst($data['first_name']);
        $data['last_name'] = strtoupper($data['last_name']);
        $data['email'] = strtolower($data['email']);

        return $data;
    }
}
