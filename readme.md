# 🌱 AgroSolutions — Hackathon FIAP 8NETT (Fase 5)

> Plataforma MVP de Agricultura 4.0 para modernizar cooperativas com IoT, análise de dados em tempo real e alertas inteligentes para agricultura de precisão.

[![.NET](https://img.shields.io/badge/.NET_8-512BD4?style=flat-square&logo=dotnet&logoColor=white)](#)
[![Docker](https://img.shields.io/badge/Docker-2496ED?style=flat-square&logo=docker&logoColor=white)](#)
[![Kubernetes](https://img.shields.io/badge/Kubernetes-326CE5?style=flat-square&logo=kubernetes&logoColor=white)](#)
[![RabbitMQ](https://img.shields.io/badge/RabbitMQ-FF6600?style=flat-square&logo=rabbitmq&logoColor=white)](#)
[![InfluxDB](https://img.shields.io/badge/InfluxDB-22ADF6?style=flat-square&logo=influxdb&logoColor=white)](#)
[![GitHub Actions](https://img.shields.io/badge/GitHub_Actions-2088FF?style=flat-square&logo=github-actions&logoColor=white)](#)

---

## 📋 Sobre o Projeto

O desafio proposto pelo Hackathon FIAP 8NETT foi modernizar a cooperativa **AgroSolutions**, substituindo processos manuais por uma solução baseada em dados em tempo real — reduzindo desperdícios e aumentando a produtividade dos produtores rurais.

A solução entregue é um MVP funcional com autenticação, cadastro de propriedades e talhões, ingestão de dados de sensores, motor de alertas e dashboard de monitoramento.

**Minha responsabilidade:** Identity API (autenticação JWT) e Property Management API (cadastro de propriedades e talhões).

---

## 🏗️ Arquitetura

```
Sensor simulado
      │
      ▼
┌─────────────┐     ┌──────────────┐     ┌───────────────────┐
│ Ingestion   │────▶│  RabbitMQ    │────▶│ Analytics/Alerts  │
│     API     │     │ (mensageria) │     │     Worker        │
└─────────────┘     └──────────────┘     └────────┬──────────┘
                                                   │
                                                   ▼
┌─────────────┐     ┌──────────────┐     ┌───────────────────┐
│  Identity   │     │   Property   │     │     InfluxDB      │
│   Service   │     │   Service    │     │  (série temporal) │
└─────────────┘     └──────────────┘     └────────┬──────────┘
       │                   │                       │
       └──────────┬────────┘                       ▼
                  ▼                        ┌───────────────────┐
            SQL Server                     │      Grafana      │
         (fora do cluster)                 │    (dashboard)    │
                                           └───────────────────┘
```

Todos os serviços rodam em um cluster **Kubernetes local (kind)**, exceto o SQL Server que é provisionado via `docker-compose` e acessado dentro do cluster via `Service + EndpointSlice`.

---

## 🛠️ Stack e Tecnologias

| Categoria | Tecnologia |
|---|---|
| Backend | .NET 8, C#, ASP.NET Core |
| Autenticação | JWT Bearer |
| Banco Relacional | SQL Server (docker-compose, fora do cluster) |
| Banco de Série Temporal | InfluxDB (TSDB) |
| Mensageria | RabbitMQ |
| Observabilidade | Grafana + Prometheus/Zabbix |
| Containerização | Docker, Docker Compose |
| Orquestração | Kubernetes (kind — cluster local) |
| CI/CD | GitHub Actions (runners self-hosted) |

### Por que InfluxDB?

Bancos relacionais têm custo desproporcional para ingestão contínua de telemetria. O InfluxDB foi escolhido por ser uma **Time Series Database (TSDB)** otimizada para dados temporais, oferecendo:

- Alta compressão via arquitetura **TSM + TSI**
- Retenção configurável e downsampling automático
- Baixa latência para alertas críticos em tempo real
- Linguagem **Flux** para agregações no banco, reduzindo carga nos serviços .NET
- Integração nativa com Grafana
- Escalabilidade horizontal (cloud-native)

---

## 🎯 Funcionalidades

**Requisitos funcionais**
- [x] Login do produtor (e-mail/senha) com JWT
- [x] Cadastro de propriedade e talhões (com cultura por talhão)
- [x] API de ingestão de sensores simulados (umidade, temperatura, precipitação)
- [x] Dashboard com histórico e status geral do talhão
- [x] Motor de alertas simples (ex.: umidade < 30% por 24h → "Alerta de Seca")

**Requisitos técnicos obrigatórios**
- [x] Arquitetura de microsserviços
- [x] Kubernetes (kind local)
- [x] Observabilidade (Grafana + Prometheus/Zabbix)
- [x] Mensageria (RabbitMQ)
- [x] CI/CD (GitHub Actions)

---

## ⚙️ Como Rodar Localmente

### Pré-requisitos

- [Docker + Docker Compose](https://www.docker.com/)
- [kubectl](https://kubernetes.io/docs/tasks/tools/)
- [kind](https://kind.sigs.k8s.io/)
- (opcional) [Helm](https://helm.sh/)

### 1. Subir o SQL Server fora do cluster

```bash
docker compose up -d
```

### 2. Criar o cluster Kubernetes local

```bash
kind create cluster --name agrosolutions
```

### 3. Aplicar os manifests Kubernetes

```bash
kubectl apply -f k8s/
```

### 4. Verificar os pods

```bash
kubectl get pods
```

### 5. Acessar os serviços via port-forward

```bash
# Identity API
kubectl port-forward svc/identity-service 5001:80

# Property API
kubectl port-forward svc/property-service 5002:80

# Grafana
kubectl port-forward svc/grafana 3000:3000
```

---

## 📡 Endpoints Principais

### Identity API
| Método | Rota | Descrição |
|---|---|---|
| POST | `/api/auth/register` | Cadastro do produtor |
| POST | `/api/auth/login` | Login — retorna JWT |

### Property API
| Método | Rota | Descrição |
|---|---|---|
| POST | `/api/properties` | Cadastra propriedade |
| GET | `/api/properties` | Lista propriedades |
| POST | `/api/properties/{id}/plots` | Cadastra talhão |
| GET | `/api/properties/{id}/plots` | Lista talhões da propriedade |

### Ingestion API
| Método | Rota | Descrição |
|---|---|---|
| POST | `/api/sensors/readings` | Envia leitura de sensor simulado |

> Para documentação completa, acesse `/swagger` após rodar cada serviço.

---

## 🔄 Fluxo de Dados

```
1. Sensor simulado envia leitura → Ingestion API
2. Ingestion API publica evento no RabbitMQ
3. Analytics Worker consome a mensagem
4. Worker persiste série temporal no InfluxDB
5. Worker aplica regras de alerta (ex.: umidade < 30% por 24h)
6. Grafana consulta InfluxDB e exibe histórico + alertas no dashboard
```

---

## 🚀 CI/CD

Pipeline via **GitHub Actions** com runners self-hosted:
- Build e testes automáticos em cada Pull Request
- Build e push das imagens Docker
- Deploy automático dos manifests Kubernetes

---

## 📸 Diagramas

![Login e Cadastro](docs/login_cadastro.png)
![Ingestão e Dashboard](docs/ingestao_dashboard.png)

---

## 👥 Equipe

| Nome | GitHub | RM | Responsabilidade |
|---|---|---|---|
| Marlon Santos Limeira | [@marlonsantoslimeira](https://github.com/marlonsantoslimeira) | RM361866 | Identity API, Property API |
| Armando José Oliveira | [@armandojoseoliveira](https://github.com/armandojoseoliveira) | RM361112 | Ingestion API |
| Matheus Nascimento Costa | — | RM363404 | Analytics/Alerts Worker |
| Ricardo Noronha de Menezes | [@ricardonoronha](https://github.com/ricardonoronha) | RM363183 | Kubernetes, CI/CD, Infra |

---

## 📄 Contexto Acadêmico

Projeto desenvolvido para o **Hackathon FIAP 8NETT — Fase 5**
Pós Tech — Arquitetura de Sistemas .NET | FIAP + Alura + PM3

---

<div align="center">
Feito com ☕ e muita agricultura de precisão 🌾
</div>
