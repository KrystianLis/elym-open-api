#!/bin/bash

cd OpenApi

echo "Building the OpenApi in Release mode..."
dotnet build -c Release

echo "Running E2E test..."
dotnet test tests/E2E.OpenApiTests -c Release

echo "E2E test completed."