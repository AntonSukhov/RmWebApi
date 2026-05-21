#!/bin/bash

echo "🔧 Generating API client for RM.Api..."
echo "➡️ Using specification: OpenApiSpecs/RmWebApi.v1.json"

# Переход в корень проекта
cd "$(dirname "$0")/.." || exit 1

# Восстановление .NET‑инструментов
echo "🛠️ Restoring .NET tools..."
dotnet tool restore

# Запуск генерации через NSwag
echo "🚀 Running NSwag code generation..."
nswag run nswag.json

if [ $? -eq 0 ]; then
    echo "✅ API client generated successfully!"
    echo "➡️ Output: GeneratedApiClients/RmWebApiClient.cs"
    echo "💡 DTO classes are internal, client is internal"
else
    echo "❌ Failed to generate API client."
    exit 1
fi
