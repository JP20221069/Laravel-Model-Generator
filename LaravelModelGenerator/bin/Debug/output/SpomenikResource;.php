<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;
use App\Http\Controllers\<CONTROLLER_NAME>;
use App\Model\<MODEL_NAME>;

class <RESOURCE_NAME> extends JsonResource
{
    /**
     * Transform the resource into an array.
     *
     * @return array<string, mixed>
     */
    public function toArray(Request $request): array
    {
        return [
            <COLUMN_ASSIGNMENTS>
          ];
    }
}