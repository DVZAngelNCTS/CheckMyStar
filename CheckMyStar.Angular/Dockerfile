# Étape 1 : Build Angular
FROM node:20 AS build
WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build -- --configuration production

# Étape 2 : Serveur Nginx
FROM nginx:alpine

# Supprime la config par défaut
RUN rm /etc/nginx/conf.d/default.conf

# Copie du build Angular
COPY --from=build /app/dist /usr/share/nginx/html

# Copie d'un nginx.conf minimal pour éviter les crashs avant le bind-mount
COPY nginx.minimal.conf /etc/nginx/nginx.conf

EXPOSE 80
EXPOSE 443
