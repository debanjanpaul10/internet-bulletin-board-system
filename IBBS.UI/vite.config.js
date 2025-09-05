import { defineConfig } from "vite";
import basicSsl from "@vitejs/plugin-basic-ssl";
import react from "@vitejs/plugin-react";
import path from "path";
import mkcert from "vite-plugin-mkcert";

/**
 * Defines the vite config for React application.
 */
export default defineConfig(({ mode }) => {
    const isDev = mode === "development";

    return {
        plugins: [react(), basicSsl(), mkcert()],
        build: isDev
            ? {}
            : {
                  target: "esnext",
                  minify: "terser",
                  rollupOptions: {
                      input: path.resolve(__dirname, "index.html"),
                      external: [],
                  },
                  assetsDir: "assets",
                  copyPublicDir: true,
              },
        optimizeDeps: {
            include: [
                "react",
                "react-dom",
                "react/jsx-runtime",
                "@fluentui/react-components",
                "@fluentui/react-icons",
                "@auth0/auth0-react",
                "js-cookie",
                "axios",
                "framer-motion",
                "react-router-dom",
                "react-redux",
                "@reduxjs/toolkit",
                "@fortawesome/react-fontawesome",
                "@fortawesome/free-brands-svg-icons",
                "ogl",
            ],
            force: true,
            esbuildOptions: {
                loader: {
                    ".js": "jsx",
                    ".css": "css",
                },
            },
        },
        resolve: {
            alias: {
                "@": path.resolve(__dirname, "./"),
                "@components": path.resolve(__dirname, "./Components"),
                "@helpers": path.resolve(__dirname, "./Helpers"),
                "@store": path.resolve(__dirname, "./Store"),
                "@styles": path.resolve(__dirname, "./Styles"),
                "@models": path.resolve(__dirname, "./Models"),
                "@services": path.resolve(__dirname, "./Services"),
                "@context": path.resolve(__dirname, "./Context"),
                "@assets": path.resolve(__dirname, "./assets"),
                "@animations": path.resolve(__dirname, "./Animations"),
                "@environment": path.resolve(
                    __dirname,
                    `./environments/environment${
                        mode === "development" ? ".development" : ""
                    }.ts`
                ),
            },
            extensions: [".js", ".jsx", ".ts", ".tsx", ".json"],
        },
        server: {
            https: true,
            host: true,
            port: 5173,
            strictPort: false,
            fs: {
                allow: [".."],
            },
            hmr: {
                overlay: false,
            },
        },
        define: {
            global: "window",
            "process.env": process.env,
        },
    };
});
