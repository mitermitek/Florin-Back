<?php

namespace App\Enums;

enum TransactionType: string
{
    case EXPENSE = 'expense';
    case INCOME = 'income';
}
