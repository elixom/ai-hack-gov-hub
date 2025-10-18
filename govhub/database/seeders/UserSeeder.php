<?php

namespace Database\Seeders;

use App\Models\User;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\Hash;

class UserSeeder extends Seeder
{
    public function run(): void
    {
        $users = [
            [
                'first_name' => 'Dr. Maria',
                'last_name' => 'Rodriguez',
                'signature_credentials' => 'BSN, MD',
                'email' => 'maria.rodriguez@ccp.com',
                'password' => Hash::make('password123'),
            ],
            [
                'first_name' => 'Dr. Damalie',
                'last_name' => 'Baker',
                'signature_credentials' => 'BSN, MD',
                'email' => 'damalie.baker@ccp.com',
                'password' => Hash::make('password123'),
            ],
            [
                'first_name' => 'Dr. Sarah',
                'last_name' => 'Chen',
                'signature_credentials' => 'BSN, MD',
                'email' => 'sarah.chen@ccp.com',
                'password' => Hash::make('password123'),
            ]
        ];

        foreach ($users as $userData) {
            User::updateOrCreate($userData);
        }
    }
}
