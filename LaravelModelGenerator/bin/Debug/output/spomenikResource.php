<?php

namespace App\Http\Resources;

use Illuminate\Http\Request;
use Illuminate\Http\Resources\Json\JsonResource;
use App\Http\Controllers\spomenikController;
use App\Model\spomenik;

class spomenikResource extends JsonResource
{
    /**
     * Transform the resource into an array.
     *
     * @return array<string, mixed>
     */
    public function toArray(Request $request): array
    {
        return [
			'spomenikID'=>$this->resource->spomenikID,
			'tip'=>$this->resource->tip,
			'vrsta'=>$this->resource->vrsta,
			'naziv_sr'=>$this->resource->naziv_sr,
			'naziv_en'=>$this->resource->naziv_en,
			'subject_sr'=>$this->resource->subject_sr,
			'subject_en'=>$this->resource->subject_en,
			'udc'=>$this->resource->udc,
			'opstina'=>$this->resource->opstina,
			'mesto_adresa'=>$this->resource->mesto_adresa,
			'rb_cen_reg'=>$this->resource->rb_cen_reg,
			'datum_cen_reg'=>$this->resource->datum_cen_reg,
			'rb_lok_reg'=>$this->resource->rb_lok_reg,
			'datum_lok_reg'=>$this->resource->datum_lok_reg,
			'zavod_lok_reg'=>$this->resource->zavod_lok_reg,
			'osnov'=>$this->resource->osnov,
			'broj_datum_upis'=>$this->resource->broj_datum_upis,
			'kategorija'=>$this->resource->kategorija,
			'broj_datum_kategorija'=>$this->resource->broj_datum_kategorija,
			'granice'=>$this->resource->granice,
			'kat_parcele'=>$this->resource->kat_parcele,
			'oblik_svojine'=>$this->resource->oblik_svojine,
			'zem_knjige'=>$this->resource->zem_knjige,
			'description_sr'=>$this->resource->description_sr,
			'description_en'=>$this->resource->description_en,
			'skola_gradnje'=>$this->resource->skola_gradnje,
			'period'=>$this->resource->period,
			'period_gradnje'=>$this->resource->period_gradnje,
			'polozaj_na_karti'=>$this->resource->polozaj_na_karti,
			'putni_pravci'=>$this->resource->putni_pravci,
			'plan_zasticenog_podrucja'=>$this->resource->plan_zasticenog_podrucja,
			'situacioni_plan'=>$this->resource->situacioni_plan,
			'plan_porte'=>$this->resource->plan_porte,
			'napomena_sr'=>$this->resource->napomena_sr,
			'napomena_en'=>$this->resource->napomena_en,
			'slika'=>$this->resource->slika,
			'unesco'=>$this->resource->unesco,
			'description_creator'=>$this->resource->description_creator,
			'description_date'=>$this->resource->description_date,
			'record_owner'=>$this->resource->record_owner

          ];
    }
}