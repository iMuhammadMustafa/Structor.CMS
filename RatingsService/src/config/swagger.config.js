import swaggerJsdoc from "swagger-jsdoc";

const options = {
  definition: {
    openapi: "3.1.0",
    info: {
      title: "Ratings Service",
      version: "1.0.0",
      description: "documented with Swagger",
    },
    servers: [
      {
        url: "http://localhost:3000",
      },
    ],
  },
  apis: ["../Controllers/**/*.js"],
};

const specs = swaggerJsdoc(options);

export default specs;
