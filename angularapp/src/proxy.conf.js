const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
    ],
    target: "http://localhost:5196",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
