/* 
 Based on Advanced Programming in the Unix Envitonment, 3ed
 by R. Stevens and S. Rago
 */

#include <limits.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/time.h>

#define NUMNUM 64000000L		/* number of numbers to sort */

long nums[NUMNUM];

int compareInts(const void *arg1, const void *arg2);
double calcTime(struct timeval start);

int main()
{
	unsigned long	i;
	struct timeval	start;
	double			elapsed;

	/*
	 * Create the initial set of numbers to sort.
	 */
	srandom(1);
    for (i = 0; i < NUMNUM; i++){
		nums[i] = random();
    }
    
    /*
     * Get timing data
     */
	gettimeofday(&start, NULL);
    
    /*
     * Do serial sort
     */

    qsort(nums, NUMNUM, sizeof(long), compareInts);


    /*
     * Get and display elapsed wall time time
     */
    elapsed = calcTime(start);
    
	printf("sort took %.4f seconds\n", elapsed);

	exit(0);
}

/*
 * Compare two long integers (helper function for heapsort)
 */
int compareInts(const void *arg1, const void *arg2)
{
    long l1 = *(long *)arg1;
    long l2 = *(long *)arg2;
    
    if (l1 == l2)
        return 0;
    else if (l1 < l2)
        return -1;
    else
        return 1;
}

double calcTime(struct timeval start){
    
    long long		startusec, endusec;
    struct timeval	end;
    
    gettimeofday(&end, NULL);
    startusec = start.tv_sec * 1000000 + start.tv_usec;
    endusec = end.tv_sec * 1000000 + end.tv_usec;
    return (double)(endusec - startusec) / 1000000.0;
}
