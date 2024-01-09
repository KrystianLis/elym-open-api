#!/bin/bash

cd OpenApi

export Logging__LogLevel__Default=Warning

echo "Building the OpenApi in Release mode..."
dotnet build -c Release

echo "Running integration tests for the OpenApi..."
dotnet test tests/OpenApi.Tests.Integration -c Release

echo "CI/CD process completed."