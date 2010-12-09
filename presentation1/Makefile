all: presentation1.pdf

presentation1.pdf:
	pdflatex presentation1.tex && pdflatex presentation1.tex

clean:
	rm -f *aux *log *nav *out *snm *toc

mrproper: clean
	rm -f *pdf
