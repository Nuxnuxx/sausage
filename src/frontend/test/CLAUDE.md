# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build and Development Commands
- Build: `npm run build`
- Development: `npm run dev`
- Preview: `npm run preview`
- Type checking: `npm run type-check`
- Linting: `npm run lint`
- Formatting: `npm run format`

## Code Style Guidelines
- Indentation: 2 spaces
- Max line length: 100 characters
- Use single quotes for strings
- No semicolons
- TypeScript for type definitions
- Vue 3 Composition API with `<script setup>` syntax
- Follow DAO pattern for data access
- Use PascalCase for component names
- Use camelCase for variable/function names
- Use proper error handling with try/catch blocks
- Imports should be organized by: external dependencies, internal modules
- Prefer async/await over Promise chains
- Vue files structure: <script>, <template>, <style>