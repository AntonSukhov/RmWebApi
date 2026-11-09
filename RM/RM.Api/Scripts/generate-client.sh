#!/bin/bash

echo "🔧 Generating API client for RM.Api..."
echo "➡️ Using specification: OpenApiSpecs/RmWebApi.v1.json"

# Переход в корень проекта RM.Api
cd "$(dirname "$0")/.." || exit 1

# Определяем.major-версию .NET и сопоставляем с runtime NSwag
DOTNET_MAJOR=$(dotnet --version | cut -d. -f1)
case $DOTNET_MAJOR in
    8) NSWAG_RUNTIME="Net80" ;;
    9) NSWAG_RUNTIME="Net90" ;;
    10) NSWAG_RUNTIME="Net100" ;;
    *) echo "⚠️ Unknown .NET major version: $DOTNET_MAJOR. Trying default..."; NSWAG_RUNTIME="" ;;
esac

if [ -n "$NSWAG_RUNTIME" ]; then
    echo "📌 Detected .NET $DOTNET_MAJOR → NSwag runtime: $NSWAG_RUNTIME"
    # Временно добавляем runtime в nswag.json (первая строка после {)
    sed -i "1s/{/{\n  \"runtime\": \"$NSWAG_RUNTIME\",/" nswag.json
fi

# Восстановление .NET‑инструментов (NSwag из локального манифеста .config/dotnet-tools.json)
echo "🛠️ Restoring .NET tools..."
dotnet tool restore

if [ $? -ne 0 ]; then
    echo "❌ Failed to restore .NET tools."
    exit 1
fi

# Запуск генерации через NSwag (запускается из локального манифеста инструментов)
echo "🚀 Running NSwag code generation..."
dotnet tool run nswag run nswag.json

GENERATE_STATUS=$?

# Восстанавливаем nswag.json (убираем добавленную строку runtime)
if [ -n "$NSWAG_RUNTIME" ]; then
    sed -i '/"runtime": "\w\+"/d' nswag.json
fi

if [ $GENERATE_STATUS -eq 0 ]; then
    echo "✅ API client generated successfully!"
    echo "➡️ Output: GeneratedApiClients/RmWebApiClient.cs"
    echo "💡 DTO classes are internal, client is internal"
else
    echo "❌ Failed to generate API client."
    exit 1
fi
