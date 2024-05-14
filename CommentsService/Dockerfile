# Build stage
FROM maven:eclipse-temurin AS build
WORKDIR /app
COPY pom.xml .
COPY src ./src
RUN mvn clean package
# RUN mvn -f /app/pom.xml clean package




# Run stage
FROM eclipse-temurin:22-alpine AS run
WORKDIR /app
COPY --from=build /app/target/CommentsService*.jar app.jar
ENTRYPOINT ["java","-jar","app.jar"]