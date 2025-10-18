<?php

namespace App\Libraries;

use Illuminate\Support\Facades\Http;

class AzureAi
{
    protected $endpoint;
    protected $key;
    protected $deployment;


    public function __construct()
    {
        $this->key = config('services.azure_ai.key');
        $this->endpoint = config('services.azure_ai.endpoint');
        $this->deployment = config('services.azure_ai.deployment_name');
    }

    public function generateText(string $prompt): string
    {
        $response = Http::withHeaders([
            'api-key' => $this->key,
            'Content-Type' => 'application/json',
        ])->post("{$this->endpoint}/openai/deployments/{$this->deployment}/completions?api-version=2023-05-15", [
            'prompt'     => $prompt,
            'max_tokens' => 150,
        ]);

        if ($response->failed()) {
            throw new \Exception('Azure OpenAI error: ' . $response->body());
        }

        return $response->json()['choices'][0]['text'] ?? '';
    }
}
