# Technologies

[.NET8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0), [Golang](https://go.dev/doc/install), [Helm](https://github.com/helm/helm/releases), [Docker](https://www.docker.com/products/docker-desktop/), [Kubernetes](https://kubernetes.io/pl/docs/setup/)

# Running images with docker-compose
```
docker-compose -f docker-compose.yml build
docker-compose -f docker-compose.yml up -d
```

# Run integration tests

```
chmod +x integration-tests-script.sh

./integration-tests-script.sh
```
# Deployment on Kubernetes using Helm

This command installs a chart.
```
helm install elympics .\helm\
```

After deploying the application, run the end-to-end tests
```
chmod +x .\e2e-tests-script.sh

.\e2e-tests-script.sh
```

This command takes a release name and uninstalls the release
```
helm uninstall elympics
```
