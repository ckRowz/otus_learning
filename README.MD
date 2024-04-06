OTUS Learning

# ДЗ №1

* docs/ДЗ_1.txt: пользовательские сценарии, функциональная модель, декомпозиция
* docs/func.png: функциональная модель
* docs/services.png: сервисы, выделенные методом функционального моделирования  
* docs/src.drawio: исходники диаграмм для drawio

# ДЗ №2

## Dockerfile 
```src/VirtualAssistant/VirtualAssistant.API/Dockerfile```

## Dockerhub
```cl6wn/virtual_assistant```

# ДЗ №3

## Применить манифест
```kubectl apply -f kube/.```

## GET к сервису
```curl arch.homework/otusapp/eygerman/health```

## ingress nginx
```kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.6.4/deploy/static/provider/cloud/deploy.yaml```
