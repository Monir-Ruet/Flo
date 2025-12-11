#!/bin/bash
set -e

# github codespace setup script
sudo chmod -R 777 /home/vscode
mkdir -p /home/vscode/.vscode-remote/data/Machine
touch /home/vscode/.vscode-remote/data/Machine/settings.json
echo "{}" > /home/vscode/.vscode-remote/data/Machine/settings.json

export PATH="$PATH:/home/vscode/.dotnet/tools"

echo "📦 Installing EF Tools..."
dotnet tool install --global dotnet-ef || echo "dotnet-ef already installed"

echo "🔐 Trusting HTTPS dev certs..."
dotnet dev-certs https --trust || true

echo "🔁 Reinitializing Dapr..."
dapr uninstall --all || true
dapr init

# Resolved this error when starting shell in codespace:
mkdir -p ~/.dapr
dapr completion bash > ~/.dapr/completion.bash.inc

docker stop rabbitmq || true
docker rm rabbitmq || true
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4.1-management

echo "🔄 Applying EF migrations..."
dotnet ef database update \
  --project ./src/Identity/Orionexx.Identity.Infrastructure/ \
  --startup-project ./src/Identity/Orionexx.Identity.Grpc/

echo "✅ Setup complete."
