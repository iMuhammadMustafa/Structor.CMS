FROM node:slim
# RUN mkdir -p /home/node/app/node_modules && chown -R node:node /home/node/app
WORKDIR /ratings
COPY . .
RUN npm install
RUN npm install nodemon -g
ENV PORT 8080
EXPOSE 8080
CMD [ "npm","run", "dev" ]