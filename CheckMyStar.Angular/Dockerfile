# Étape 1 : Build Angular
FROM node:20 AS build
WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build -- --configuration production

# Étape 2 : Serveur Nginx
FROM nginx:alpine
COPY --from=build /app/dist/CheckMyStar.Angular/browser /usr/share/nginx/html

EXPOSE 80
