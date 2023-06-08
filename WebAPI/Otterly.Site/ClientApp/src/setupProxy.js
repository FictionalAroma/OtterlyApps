const { createProxyMiddleware } = require('http-proxy-middleware');

const target = "https://localhost:7161";

const context =  [
  "/bff",
];

module.exports = function(app) {
  const appProxy = createProxyMiddleware( context, {
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    },
    changeOrigin: true
  });

  app.use(appProxy);
};
