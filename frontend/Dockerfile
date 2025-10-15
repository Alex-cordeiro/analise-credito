# Etapa 1 — Build da aplicação
FROM node:18-alpine AS builder

# Define o diretório de trabalho
WORKDIR /app


COPY package*.json ./
RUN npm ci


COPY . .
RUN npm run build


FROM nginx:alpine


RUN rm -rf /usr/share/nginx/html/*

COPY --from=builder /app/dist /usr/share/nginx/html

# Copia configuração customizada (opcional)
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

# Comando de inicialização
CMD ["nginx", "-g", "daemon off;"]
