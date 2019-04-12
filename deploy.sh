docker build --rm -f "src/Dashboard/Dockerfile" -t bradygaster/dashboard:latest src/Dashboard
docker push bradygaster/dashboard:latest

docker build --rm -f "src/FactoryController/Dockerfile" -t bradygaster/factorycontroller:latest src/FactoryController
docker push bradygaster/factorycontroller:latest

kubectl apply -f src/Dashboard/k8s.yaml
kubectl apply -f src/FactoryController/k8s.yaml