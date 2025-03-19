import { defineConfig } from "vite";
import basicSsl from "@vitejs/plugin-basic-ssl";
import react from "@vitejs/plugin-react";
import path from "path";
import mkcert from "vite-plugin-mkcert";

export default defineConfig(() => {
	return {
		plugins: [react(), basicSsl(), mkcert()],
		build: {
			target: "esnext",
			minify: "terser",
			lib: {
				entry: "React/index.jsx",
				formats: ["es"],
			},
			rollupOptions: {
				input: {
					main: path.resolve(__dirname, "index.html"),
					app: path.resolve(__dirname, "React/index.jsx"),
				},
				external: [],
				output: {
					globals: {
						react: "React",
						"react-dom": "ReactDOM",
					},
				},
			},
		},
		optimizeDeps: {
			include: ["has-hover"],
			esbuildOptions: {
				loader: {
					".js": "jsx",
					".css": "css",
				},
			},
		},
		resolve: {
			alias: {
				"@": path.resolve(__dirname, "./React"),
				"@components": path.resolve("./React/Components"),
				"@helpers": path.resolve("./React/Helpers"),
				"@store": path.resolve("./React/Store"),
				"@styles": path.resolve("./React/Styles"),
				"@models": path.resolve("./React/Models"),
                "@services": path.resolve("./React/Services"),
			},
		},
		server: {
			https: true,
			port: 6970,
		},
		define: {
			global: "window",
			"process.env": {},
		},
	};
});
