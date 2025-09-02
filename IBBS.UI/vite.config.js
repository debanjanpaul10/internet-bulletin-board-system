﻿import { defineConfig } from "vite";
import basicSsl from "@vitejs/plugin-basic-ssl";
import react from "@vitejs/plugin-react";
import path from "path";
import mkcert from "vite-plugin-mkcert";

/**
 * Defines the vite config for React application.
 */
export default defineConfig(() => {
    return {
        plugins: [react(), basicSsl(), mkcert()],
        build: {
            target: "esnext",
            minify: "terser",
            lib: {
                entry: "index.jsx",
                formats: ["es"],
            },
            rollupOptions: {
                input: {
                    main: path.resolve(__dirname, "index.html"),
                    app: path.resolve(__dirname, "index.jsx"),
                },
                external: [],
                output: {
                    globals: {
                        react: "React",
                        "react-dom": "ReactDOM",
                    },
                },
            },
            assetsDir: "assets",
            copyPublicDir: true,
        },
        optimizeDeps: {
            include: [],
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
                "@components": path.resolve("./Components"),
                "@helpers": path.resolve("./Helpers"),
                "@store": path.resolve("./Store"),
                "@styles": path.resolve("./Styles"),
                "@models": path.resolve("./Models"),
                "@services": path.resolve("./Services"),
                "@context": path.resolve("./Context"),
                "@assets": path.resolve("./assets"),
                "@animations": path.resolve("./Animations"),
            },
        },
        server: {
            https: true,
        },
        define: {
            global: "window",
            "process.env": process.env,
        },
    };
});
