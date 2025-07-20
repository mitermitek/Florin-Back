<?php

namespace App\Models;

use App\Enums\TransactionType;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Transaction extends Model
{
    use HasFactory;

    protected $fillable = [
        'type',
        'date',
        'amount',
        'description'
    ];

    public function casts()
    {
        return [
            'type' => TransactionType::class,
            'date' => 'date',
            'amount' => 'decimal:2',
        ];
    }

    public function getDateAttribute($value)
    {
        return $this->asDate($value)->format('Y-m-d');
    }

    public function user()
    {
        return $this->belongsTo(User::class);
    }

    public function category()
    {
        return $this->belongsTo(Category::class);
    }
}
